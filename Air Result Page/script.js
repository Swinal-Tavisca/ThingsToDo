(function () {
        var content = document.getElementById('content');
        var data = {
            "people": [
                {
                    "City": "MumbaiAirport",
                    "ArrivalDateTime": "11092018061000",
                    "DepartureDateTime": "11092018042000",
                    "ArrivalTerminal": "1",
                    "DepartureTerminal":"1",
                    "DurationMinutes": "260",
                },
                {
                    "City": "DelhiAirport",
                    "ArrivalDateTime": "11092018061000",
                    "DepartureDateTime": "11092018042000",
                    "ArrivalTerminal": "1",
                    "DepartureTerminal":"2",
                    "DurationMinutes": "300",
                },
                {
                    "City": "PuneAirport",
                    "ArrivalDateTime": "11092018061000",
                    "DepartureDateTime": "11092018042000",
                    "ArrivalTerminal": "1",
                    "DepartureTerminal":"1",
                    "DurationMinutes": "240",
                },
                {
                    "City": "SingaporeAirport",
                    "ArrivalDateTime": "11092018061000",
                    "DepartureDateTime": "11092018042000",
                    "ArrivalTerminal": "1",
                    "DepartureTerminal":"1",
                    "DurationMinutes": "300",
                },
                
                {
                    "City": "TaipeiAirport",
                    "ArrivalDateTime": "11092018061000",
                    "DepartureDateTime": "11092018042000",
                    "ArrivalTerminal": "1",
                    "DepartureTerminal":"1",
                    "DurationMinutes": "360",
                },
      ]
        };


var template = Handlebars.compile(document.getElementById('people-template').innerHTML);

content.innerHTML = template(data);

})();
