import {Data} from "./dataModel.model";
export class DataService
{
    response:Data[];
    origin:{
        lat: number,
        lng: number
    };

    destination:{
        lat:number,
        lng:number
    }
    direction:boolean;

    getDirection(latitude: any,longitude: any) {
        this.direction=true;
        latitude=Number(latitude);
        longitude=Number(longitude);
        this.destination = { lat: latitude, lng: longitude};
      }
}