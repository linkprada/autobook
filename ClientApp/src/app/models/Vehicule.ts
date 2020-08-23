import { KeyValuePair } from './KeyValuePair';
import { Contact } from './Contact';

export interface Vehicule {
    id : number
    model : KeyValuePair ;
    make : KeyValuePair ;
    isRegistred : boolean ;
    features : KeyValuePair[] ;
    contact : Contact ;
    lastUpdated : string ; 
}