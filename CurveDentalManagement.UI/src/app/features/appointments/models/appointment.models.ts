import { Doctor } from "../../doctor/models/doctor.models";
import { Patient } from "../../patient/models/patient.models";

export interface Appointment {
    id: string;
    status: string;
    remarks: string;
    appointmentCode: string;
    appointmentDate: Date;
    appointmentTime: string;
    doctors: Doctor[];
    patients: Patient[];   

}