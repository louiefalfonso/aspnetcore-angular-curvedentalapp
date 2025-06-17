import { Patient } from "../../patient/models/patient.models";
import { Treatment } from "../../treatments/models/treatment.models";

export interface Billing {
    id: string;
    
    patients: Patient[];
    treatments: Treatment[];   
}