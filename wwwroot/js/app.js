let tries;
let statObjs = [];
function GetAnswer ()
{
    if (GamePlay.countries.length > 0){
        tries = 0;
        let randNum = Math.round(Math.random() * GamePlay.countries.length - 1);
        let country = GamePlay.countries.splice(randNum, 1)[0];
        $('.answer').html(country.Name);
        return country;
    } else{
        submit();
    }
    
}

function submit (){
    $.ajax({
        type: "POST",
        url: "http://localhost:5000/Stats/Create",
        data: {stats: statObjs}
    }).done((e) =>{
        console.log("something");
        console.log(typeof e);
        if (e === "success") {
            console.log("something else");
            window.location.href = 'http://localhost:5000/Stats';
        }
    }).fail((a,b,c) => {
        console.log("Failed")
    })
}

let country = GetAnswer();

$(".submit").click(submit);

$("#Map").click((e) => {
    if(e.target.tagName === "AREA"){
        tries++;
        var target = e.target;
        if (parseInt(target.title) === parseInt(country.CountryId)){
            statObjs.push({
                CountryId: country.CountryId,
                Tries: tries,
                Success: 1
            });
            console.log("right answer");
            country = GetAnswer();
            
        } else {
            if (tries > 4){
                statObjs.push({
                    CountryId: country.CountryId,
                    Tries: tries,
                    Success: 0
                });
                console.log("5 tries is too many");
                country = GetAnswer();
                
            } else {
                console.log("wrong answer, try again");
            }     
        } 
        console.log(statObjs);      
    }
});
