export interface AddAppointmentRequest {
    status: string;
    remarks: string;
    appointmentCode: string;
    appointmentDate: Date;
    appointmentTime: string;
    doctors: string[];
    patients: string[];
}