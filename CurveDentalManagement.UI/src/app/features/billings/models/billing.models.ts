import { Patient } from "../../patient/models/patient.models";
import { Treatment } from "../../treatments/models/treatment.models";

export interface Billing {
    id: string;
    billingCode: string;
    totalAmount: string;
    paymentStatus: string;
    paymentMethod: string;
    biilingDate: Date;
    paymentDate: Date;
    remarks: string;
    patients: Patient[];
    treatments: Treatment[];   
}