import { Contact } from './Contact';

export interface SaveVehicule {
    id : number
    modelId : number ;
    makeId : number ;
    isRegistred : boolean ;
    features : number[] ;
    contact : Contact ;
}