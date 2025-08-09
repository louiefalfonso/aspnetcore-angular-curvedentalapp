import { Patient } from "../../patient/models/patient.models";
import { Treatment } from "../../treatments/models/treatment.models";

export interface Billing {
    id: string;
    billingCode: string;
    totalAmount: string;
    paymentStatus: string;
    paymentMethod: string;
    billingStatus: string;
    billingDate: Date;
    paymentDate: Date;
    remarks: string;
    patients: Patient[];
    treatments: Treatment[];   
}

    