export interface UpdateAppointmentRequest{
    status: string;
    remarks: string;
    appointmentCode: string;
    appointmentDate: Date;
    appointmentTime: string;
    doctors: string[];
    patient: string[];
}