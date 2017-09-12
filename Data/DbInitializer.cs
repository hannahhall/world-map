using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldMap.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WorldMap.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Continent.Any())
                {
                    var continents = new Continent[]
                    {
                        new Continent(){Name="Europe"},
                        new Continent(){Name="Oceania"},
                        new Continent(){Name="Asia"},
                        new Continent(){Name="Africa"},
                        new Continent(){Name="North America"},
                        new Continent(){Name="South America"},
                    };
                    foreach (Continent c in continents)
                    {
                        context.Continent.Add(c);
                    }
                    context.SaveChanges();
                }

                if (!context.SubRegion.Any())
                {
                    var europe = context.Continent.SingleOrDefault(c => c.Name == "Europe");
                    var asia = context.Continent.SingleOrDefault(c => c.Name == "Asia");
                    var africa = context.Continent.SingleOrDefault(c => c.Name == "Africa");
                    var nAmerica = context.Continent.SingleOrDefault(c => c.Name == "North America");
                    var sAmerica = context.Continent.SingleOrDefault(c => c.Name == "South America");
                    var oceania = context.Continent.SingleOrDefault(c => c.Name == "Oceania");
                    var subRegions = new SubRegion[]
                    {
                        new SubRegion(){Name = "Eastern Europe", ContinentId = europe.ContinentId},
                        new SubRegion(){Name = "Northern Europe", ContinentId = europe.ContinentId},
                        new SubRegion(){Name = "Southern Europe", ContinentId = europe.ContinentId},
                        new SubRegion(){Name = "Western Europe", ContinentId = europe.ContinentId},
                        new SubRegion(){Name = "Central Asia", ContinentId = asia.ContinentId},
                        new SubRegion(){Name = "Eastern Asia", ContinentId = asia.ContinentId},
                        new SubRegion(){Name = "Southern Asia", ContinentId = asia.ContinentId},
                        new SubRegion(){Name = "South-Eastern Asia", ContinentId = asia.ContinentId},
                        new SubRegion(){Name = "Western Asia", ContinentId = asia.ContinentId},
                        new SubRegion(){Name = "Eastern Africa", ContinentId = africa.ContinentId},
                        new SubRegion(){Name = "Middle Africa", ContinentId = africa.ContinentId},
                        new SubRegion(){Name = "Northern Africa", ContinentId = africa.ContinentId},
                        new SubRegion(){Name = "Southern Africa", ContinentId = africa.ContinentId},
                        new SubRegion(){Name = "Western Africa", ContinentId = africa.ContinentId},
                        new SubRegion(){Name = "Caribbean", ContinentId = nAmerica.ContinentId},
                        new SubRegion(){Name = "Western", ContinentId = sAmerica.ContinentId},
                        new SubRegion(){Name = "Northern", ContinentId = sAmerica.ContinentId},
                        new SubRegion(){Name = "Southern", ContinentId = sAmerica.ContinentId},
                        new SubRegion(){Name = "Melanesia", ContinentId = oceania.ContinentId},
                        new SubRegion(){Name = "Micronesia", ContinentId = oceania.ContinentId},
                        new SubRegion(){Name = "Polynesia", ContinentId = oceania.ContinentId}
                    };
                    
                    foreach (SubRegion sr in subRegions)
                    {
                        context.SubRegion.Add(sr);
                    }
                    context.SaveChanges();
                }

                if (!context.Country.Any())
                {
                    var europe = context.Continent.SingleOrDefault(c => c.Name == "Europe");
                    var east = context.SubRegion.SingleOrDefault(sr => sr.Name == "Eastern Europe");
                    var west = context.SubRegion.SingleOrDefault(sr => sr.Name == "Western Europe");
                    var north = context.SubRegion.SingleOrDefault(sr => sr.Name == "Northern Europe");
                    var south = context.SubRegion.SingleOrDefault(sr => sr.Name == "Southern Europe");

                    var europeanCountries = new Country[]
                    {
                        new Country()
                        {
                            Capital = "Mariehamn",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Åland Islands",
                            Coordinates = "605,300,615,300,619,314,609,316",
                        },
                        new Country()
                        {
                            Capital = "Tirana",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Albania",
                            Coordinates = "598,788,612,769,627,791,636,822,619,846,600,827",
                        },
                        new Country()
                        {
                            Capital = "Andorra la Vella",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Andorra",
                            Coordinates = "257,721,266,722,267,730,257,731",
                        },
                        new Country()
                        {
                            Capital = "Vienna",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Austria",
                            Coordinates = "433,629,488,629,510,600,559,607,557,635,546,658,503,667,438,646",
                        },
                        new Country()
                        {
                            Capital = "Minsk",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Belarus",
                            Coordinates = "701,462,710,422,764,411,808,471,791,476,801,503,783,526,674,528,667,512,677,493,668,469",
                        },
                        new Country()
                        {
                            Capital = "Brussels",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Belgium",
                            Coordinates = "330,513,371,513,385,544,374,552,374,564,351,549",
                        },
                        new Country()
                        {
                            Capital = "Sarajevo",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Bosnia and Herzegovina",
                            Coordinates = "584,769,534,712,537,698,546,704,554,699,602,709,598,726,605,731",
                        },
                        new Country()
                        {
                            Capital = "Sofia",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Bulgaria",
                            Coordinates = "783,726,772,776,731,783,728,791,671,799,671,784,660,778,661,759,670,754,660,739,666,726,723,734,740,,721"
                        },
                        new Country()
                        {
                            Capital = "Zagreb",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Croatia",
                            Coordinates = "567,775,494,692,528,690,548,668,572,686,590,682,602,709,552,699,538,699,538,717",
                        },
                        new Country()
                        {
                            Capital = "Nicosia",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Cyprus",
                            Coordinates = "876,940,919,935,911,959,880,957",
                        },
                        new Country()
                        {
                            Capital = "Prague",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Czech Republic",
                            Coordinates = "485,564,523,547,553,560,556,573,562,565,593,585,561,609,528,598,513,606",
                        },
                        new Country()
                        {
                            Capital = "Copenhagen",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Denmark",
                            Coordinates = "433,432,430,382,484,356,499,402,501,450",
                        },
                        new Country()
                        {
                            Capital = "Tallinn",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Estonia",
                            Coordinates = "720,321,644,334,636,367,672,361,691,363,712,372",
                        },
                        new Country()
                        {
                            Capital = "Tórshavn",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Faroe Islands",
                            Coordinates = "279,192,305,191,303,215,275,225",
                        },
                        new Country()
                        {
                            Capital = "",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Finland",
                            Coordinates = "678,50,690,56,693,85,710,107,702,125,720,150,717,160,729,189,728,204,750,227,711,293,656,318,630,311,,623,241,645,214,663,187,672,179,664,164,654,156,644,105,618,77,624,70,641,85,651,83,660,88,670,62,671,54"
                        },
                        new Country()
                        {
                            Capital = "Paris",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "France",
                            Coordinates = "328,516,183,540,206,610,208,688,245,717,291,732,308,714,348,730,368,720,373,706,376,675,374,651,360,,652,376,630,384,621,395,621,400,605,412,587,387,576,365,565"
                        },
                        new Country()
                        {
                            Capital = "Berlin",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Germany",
                            Coordinates = "445,433,461,440,504,457,525,475,526,548,486,560,507,603,485,614,491,630,477,628,434,625,415,617,397,,622,414,590,385,568,384,547,387,523,387,510,401,508,409,487,417,465"
                        },
                        new Country()
                        {
                            Capital = "Gibraltar",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Gibraltar",
                            Coordinates = "89,852,89,847,95,846,96,852",
                        },
                        new Country()
                        {
                            Capital = "Athens",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Greece",
                            Coordinates = "620,847,638,816,672,799,735,784,742,793,732,810,712,887,791,922,740,967,688,957,628,896",
                        },
                        new Country()
                        {
                            Capital = "St. Peter Port",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Guernsey",
                            Coordinates = "231,528,236,529,236,535,230,539",
                        },
                        new Country()
                        {
                            Capital = "Vatican City",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Vatican City",
                            Coordinates = "463,772,468,776,467,781,464,779",
                        },
                        new Country()
                        {
                            Capital = "Budapest",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Hungary",
                            Coordinates = "563,627,581,634,596,631,596,622,612,616,615,620,624,611,641,610,645,614,652,614,664,627,653,632,622,,675,606,673,601,680,575,684,547,661,553,641"
                        },
                        new Country()
                        {
                            Capital = "Reykjavík",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Iceland",
                            Coordinates = "133,27,272,32,274,126,139,138",
                        },
                        new Country()
                        {
                            Capital = "Dublin",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Ireland",
                            Coordinates = "203,373,197,386,203,391,210,388,220,400,199,450,139,444,129,418,160,369,205,357",
                        },
                        new Country()
                        {
                            Capital = "Douglas",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Isle of Man",
                            Coordinates = "237,403,247,398,253,407,241,412",
                        },
                        new Country()
                        {
                            Capital = "Rome",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Italy",
                            Coordinates = "382,720,379,711,371,703,373,691,368,682,378,676,375,664,393,665,403,657,411,668,421,657,433,661,437,,656,441,656,446,645,472,644,476,652,500,663,498,672,501,686,478,683,472,696,470,713,587,824,515,929,453,888,481,839,437,766,412,730"
                        },
                        new Country()
                        {
                            Capital = "Saint Helier",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Jersey",
                            Coordinates = "236,536,241,538,242,543,238,544",
                        },
                        new Country()
                        {
                            Capital = "Riga",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Latvia",
                            Coordinates = "669,365,685,360,698,372,712,372,719,375,719,383,730,396,730,407,723,417,705,419,680,403,639,406,630,,414,624,373"
                        },
                        new Country()
                        {
                            Capital = "Vaduz",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Liechtenstein",
                            Coordinates = "429,633,425,637,427,643,432,642",
                        },
                        new Country()
                        {
                            Capital = "Vilnius",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Lithuania",
                            Coordinates = "625,415,632,415,639,407,682,404,709,419,709,433,706,439,700,442,699,462,667,469,665,460,653,460,653,,444,627,437"
                        },
                        new Country()
                        {
                            Capital = "Luxembourg City",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Luxembourg",
                            Coordinates = "427,633,431,633,432,640,427,640",
                        },
                        new Country()
                        {
                            Capital = "Skopje",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Macedonia (the former Yugoslav Republic of)",
                            Coordinates = "626,791,629,781,636,781,644,778,657,776,662,780,670,782,671,798,666,807,654,807,646,813,640,813,631,,818,626,801"
                        },
                        new Country()
                        {
                            Capital = "Valletta",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Malta",
                            Coordinates = "486,927,505,936,504,946,491,943",
                        },
                        new Country()
                        {
                            Capital = "Chișinău",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Moldova (Republic of)",
                            Coordinates = "743,608,754,615,765,612,768,619,774,618,774,629,794,654,777,656,777,664,765,683,761,667,762,653,755,,645,747,632,738,619,738,614"
                        },
                        new Country()
                        {
                            Capital = "Monaco",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Monaco",
                            Coordinates = "372,723,374,717,378,722,377,726",
                        },
                        new Country()
                        {
                            Capital = "Podgorica",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Montenegro",
                            Coordinates = "583,773,596,744,622,762,614,763,616,768,612,771,607,768,600,781,600,789",
                        },
                        new Country()
                        {
                            Capital = "Amsterdam",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Netherlands",
                            Coordinates = "414,478,405,490,406,499,402,506,385,508,388,518,382,532,375,516,363,510,344,513,340,501,387,457,408,,464"
                        },
                        new Country()
                        {
                            Capital = "Oslo",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Norway",
                            Coordinates = "691,74,710,53,707,16,593,30,527,113,441,228,411,301,417,338,432,353,467,348,499,331,514,298,518,257,,522,215,531,204,542,205,542,186,555,149,574,127,577,111,585,102,590,106,595,91,613,96,618,78,624,71,641,86,649,82,657,88,668,79,670,62,676,53,686,52"
                        },
                        new Country()
                        {
                            Capital = "Warsaw",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Poland",
                            Coordinates = "526,475,527,548,553,559,559,569,563,566,575,564,597,586,602,582,613,593,622,590,626,591,639,589,663,,597,661,582,681,559,668,518,677,500,663,466,656,461,608,455"
                        },
                        new Country()
                        {
                            Capital = "Lisbon",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Portugal",
                            Coordinates = "64,682,79,678,79,686,105,695,107,708,93,717,88,733,78,745,72,745,75,765,68,771,67,782,62,788,46,817,11,,802,29,746"
                        },
                        new Country()
                        {
                            Capital = "Pristina",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Republic of Kosovo",
                            Coordinates = "614,766,623,762,628,752,647,767,638,780,634,777,627,788",
                        },
                        new Country()
                        {
                            Capital = "Bucharest",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Romania",
                            Coordinates = "800,687,783,728,749,721,732,723,720,737,670,732,664,716,657,713,644,713,622,675,631,674,655,630,664,,627,669,625,696,625,700,630,717,624,736,616,749,635,761,654,760,670,764,683,771,689,785,682"
                        },
                        new Country()
                        {
                            Capital = "Moscow",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Russian Federation",
                            Coordinates = "911,5,714,51,693,78,709,106,704,125,718,149,727,177,730,202,753,227,714,291,721,324,717,373,738,407,,759,411,774,413,802,462,805,479,799,490,801,504,834,492,848,517,859,517,878,536,900,529,953,540,959,584,936,607,919,680,984,698,1072,680,1140,700,1154,680,1097,633,1130,557,1056,528,1062,475,1103,424,1131,407,1160,411,1161,7"
                        },
                        new Country()
                        {
                            Capital = "City of San Marino",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "San Marino",
                            Coordinates = "467,724,473,725,473,732,465,732",
                        },
                        new Country()
                        {
                            Capital = "Belgrade",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Serbia",
                            Coordinates = "594,682,623,676,637,703,637,712,652,718,656,713,665,717,662,723,661,734,670,753,661,759,660,776,646,,776,649,767,629,751,623,762,599,744,605,740,605,732,597,723,602,708"
                        },
                        new Country()
                        {
                            Capital = "Bratislava",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Slovakia",
                            Coordinates = "656,599,651,614,636,608,625,608,615,619,610,617,596,621,593,633,570,631,559,615,562,604,576,602,594,,588,598,589,605,583,614,595,625,590,640,590"
                        },
                        new Country()
                        {
                            Capital = "Ljubljana",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Slovenia",
                            Coordinates = "502,667,516,668,524,663,546,657,546,672,533,674,535,683,529,688,526,694,514,687,510,692,496,692,504,,684,498,677,498,667"
                        },
                        new Country()
                        {
                            Capital = "Madrid",
                            SubRegionId = south.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Spain",
                            Coordinates = "86,633,204,690,240,719,258,734,270,732,295,739,307,839,198,855,65,836,55,807,69,786,75,763,73,748,81,,748,109,711,106,696,81,687,52,674,69,646"
                        },
                        new Country()
                        {
                            Capital = "Stockholm",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Sweden",
                            Coordinates = "617,77,642,99,655,156,632,213,606,276,614,342,593,400,540,432,506,432,502,402,492,335,502,331,519,296,,521,272,515,261,516,223,529,205,543,205,541,189,551,173,553,149,564,144,571,119,587,104,591,104,596,93,613,92"
                        },
                        new Country()
                        {
                            Capital = "Bern",
                            SubRegionId = west.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Switzerland",
                            Coordinates = "617,77,642,99,655,156,632,213,606,276,614,342,593,400,540,432,506,432,502,402,492,335,502,331,519,296,,521,272,515,261,516,223,529,205,543,205,541,189,551,173,553,149,564,144,571,119,587,104,591,104,596,93,613,92"
                        },
                        new Country()
                        {
                            Capital = "Kiev",
                            SubRegionId = east.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "Ukraine",
                            Coordinates = "672,529,681,531,684,524,782,526,787,510,811,507,812,501,837,495,844,515,851,517,857,517,867,524,875,,536,891,536,900,528,950,541,946,566,955,567,955,585,942,587,933,598,933,608,925,620,916,637,916,673,874,703,791,683,780,681,773,690,766,685,771,678,778,668,776,655,783,658,796,652,789,641,775,629,777,618,752,611,739,609,738,618,723,618,719,623,701,631,694,626,664,624,651,613,657,598,665,598,659,584,681,560"
                        },
                        new Country()
                        {
                            Capital = "London",
                            SubRegionId = north.SubRegionId,
                            ContinentId = europe.ContinentId,
                            Name = "United Kingdom of Great Britain and Northern Ireland",
                            Coordinates = "207,368,201,374,201,378,197,381,204,391,212,389,217,399,224,411,199,459,179,506,302,522,331,471,296,,374,318,317,259,277,236,296"
                        }
                    };

                    foreach (Country c in europeanCountries)
                    {
                        context.Country.Add(c);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}