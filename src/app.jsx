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

const Header = ({answer, country}) => {
  return (
    <div>
      <button onClick={() => {answer()}}>Where's that country</button>
      <h1>{country.name}</h1>

    </div>
  )
}

class MapApp extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      countries: [],
      answer: {}
    };
    this.apiUrl = 'https://restcountries.eu/rest/v2/region/Europe';

    this.getCountries = () => {
      axios.get(this.apiUrl)
        .then((res) => {
          res.data.forEach((country, index) => {
            country.id = index;
          })
          console.log("res.data", res.data);
          this.setState({countries: res.data});
        })
    }

  }
  componentDidMount(){
    this.getCountries();
  }

  getAnswer() {
    let randNum = Math.round(Math.random() * this.state.countries.length);
    let country = this.state.countries[randNum];
    this.setState({answer: country});
  }

  guess(id){
    if (id === this.state.answer.id) {
      console.log("right answer");
    } else {
      console.log('wrong answer');
    }
  }

  render() {
    return (
      <div>
        <Header
          answer={this.getAnswer.bind(this)}
          country={this.state.answer}
        />
        <CountryList
          countries={this.state.countries}
          guess={this.guess.bind(this)}
        />
      </div>
    )
  }
}

ReactDOM.render(<MapApp />, document.getElementById('container'));
