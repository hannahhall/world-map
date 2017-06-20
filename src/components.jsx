const React = require('react');

const Country = ({country, guess}) => {
  // each country
  return (
    <area onClick={() => {guess(country.id)}} alt="" title={country.name} href="#" shape="poly" coords={country.coords} />
  );
};

const CountryList = ({countries, guess}) => {
  const countryNode = countries.map((country) => {
    return (
      <Country country={country} key={country.id} guess={guess} />
    );
  });
  let style = {
    width: '70%'
  }
  return(
    <div className="mapArea" style={style}>
      <img src="europe.gif" alt="" useMap="#Map" />
      <map name="Map" id="Map">
          {countryNode}
      </map>
    </div>
  );
};

const Header = ({answer, country}) => {
  return (
    <div>
      <button onClick={() => {answer()}}>Where's that country</button>
      <h1>{country.name}</h1>

    </div>
  )
}

export {Header, CountryList, Country}
