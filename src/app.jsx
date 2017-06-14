const React = require('react');
const ReactDOM = require('react-dom');
const axios = require ('axios');

const Country = ({country, guess}) => {
  // each country
  return (
    <div onClick={() => {guess(country.id)}}>{country.name}</div>
  );
};

const CountryList = ({countries, guess}) => {
  const countryNode = countries.map((country) => {
    return (
      <Country country={country} key={country.id} guess={guess} />
    );
  });
  return(
    <div>{countryNode}</div>
  );
};

class MapApp extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      countries: []
    };
    this.apiUrl = 'https://restcountries.eu/rest/v2/region/Europe';
    this.getCountries = () => {
      axios.get(this.apiUrl)
        .then((res) => {
          res.data.forEach((country, index) => {
            country.id = index;
          })
          this.setState({countries: res.data});
        })
    }
  }
  componentDidMount(){
    this.getCountries();
  }

  guess(id){
    console.log("guess id:", id);
  }

  render() {
    return (
      <CountryList
        countries={this.state.countries}
        guess={this.guess.bind(this)}
      />
    )
  }
}

ReactDOM.render(<MapApp />, document.getElementById('container'));
