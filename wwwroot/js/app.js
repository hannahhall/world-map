let tries;
function GetAnswer ()
{
    if (GamePlay.countries.length > 0){
        tries = 0;
        let randNum = Math.round(Math.random() * GamePlay.countries.length - 1);
        let country = GamePlay.countries.splice(randNum, 1)[0];
        $('.answer').html(country.Name);
        return country;
    } else{
        $(".results").modal('show');
    }
    
}

function submit (obj){
    $.ajax({
        type: "POST",
        url: "http://localhost:5000/Stats/Create",
        data: {stats: obj}
    }).done((e) =>{
        console.log("something");
        console.log(typeof e);
        if (e === "success") {
            console.log("something else");
            country = GetAnswer();
        }
    }).fail((a,b,c) => {
        console.log(a, b, c);
    })
}

let country = GetAnswer();

$(".submit").click(submit);

$("#Map").click((e) => {
    if(e.target.tagName === "AREA"){
        tries++;
        let target = e.target;
        if (parseInt(target.title) === parseInt(country.CountryId)){
            submit({
                CountryId: country.CountryId,
                Tries: tries,
                Success: 1
            });
            $(".score").html("");
            alert("That was correct");
        } else {
            if (tries > 4){
                submit({
                    CountryId: country.CountryId,
                    Tries: tries,
                    Success: 0
                });
                $(".score").html("");
                alert("5 tries is too many. Moving on...");
            } else {
                $(".score").html("Wrong");
            }     
        } 
    }
});
