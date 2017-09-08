const React = require('react');
const ReactDOM = require('react-dom');
const axios = require ('axios');
const components = require('./components.jsx')

class MapApp extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      countries: [],
      answer: {},
      hint: '',
      tries: 0
    };
    this.apiUrl = 'https://restcountries.eu/rest/v2/region/Europe';
  }

  getCountries() {
    axios.all([this.getCountriesProm(), this.getCountryCoords()])
      .then(axios.spread((countries, coordsData) => {
        countries.data.forEach((country, index) => {
          country.id = index;
          coordsData.data.europe.forEach((coord) => {
            if (country.name === coord.name) {
              country.coords = coord.coords
            }
          })
        })
        console.log("countries.data", countries.data);
        this.setState({countries: countries.data});
      })
    );
  }

  componentDidMount(){
    this.getCountries();
  }

  getCountriesProm(){
    return axios.get(this.apiUrl);
  }

  getCountryCoords(){
    return axios.get('europe_coords.json');
  }

  getAnswer() {
    let randNum = Math.round(Math.random() * this.state.countries.length);
    let country = this.state.countries[randNum];
    this.setState({answer: country, tries: 0, hint: ''});
  }

  guess(id){
    console.log("id", id);
    if (id === this.state.answer.id) {
      console.log("right answer");
      this.getAnswer()
    } else if (this.state.tries === 3) {
      this.setState({hint: 'This answer is in ' + this.state.answer.subregion});
      console.log('This answer is in ' + this.state.answer.subregion);
      this.setState({tries: this.state.tries+1});
    } else if (this.state.tries === 5) {
      console.log("You lose");
      this.getAnswer();
    } else {
      this.setState({tries: this.state.tries+1});
    }
  }

  render() {
    return (
      <div>
        <components.Header
          answer={this.getAnswer.bind(this)}
          country={this.state.answer}
        />
        <components.CountryList
          countries={this.state.countries}
          guess={this.guess.bind(this)}
        />
      </div>
    )
  }
}

ReactDOM.render(<MapApp />, document.getElementById('container'));
