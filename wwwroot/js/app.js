


let randNum = Math.round(Math.random() * GamePlay.countries.length);
let country = GamePlay.countries.splice(randNum, 1)[0];
console.log(country);
$('.answer').html(country.Name);

$("#Map").click((e) => {
    if(e.target.tagName === "AREA")
    {
        var target = e.target;
        if (parseInt(target.title) === parseInt(country.CountryId))
        {
            console.log("right answer");
        } else {
            console.log("wrong answer");
        }       
    }
});
