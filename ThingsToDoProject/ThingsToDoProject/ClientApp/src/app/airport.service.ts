export class Airport {
    area: string = 'InsideOutsideAirport';
    input: string;
    test:{}[]=[{name:'shreea',age:23}];

    getInput() {
        return this.input;
    }

    setArea(area) {
        this.area = area; 
    }

    setInput(input) {
         this.input = input; 
    }
}