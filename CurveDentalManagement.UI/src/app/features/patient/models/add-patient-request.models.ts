export interface AddPatientRequest {
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    gender: string;
    email: string;
    age: string;
    phoneNumber: string;
    address: string;
    insuranceDetails: string;
    insuranceProvider: string;
    insurancePolicyNumber: string;
    insuranceExpiryDate: Date;
}