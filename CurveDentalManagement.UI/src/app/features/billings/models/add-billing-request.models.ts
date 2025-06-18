export interface AddBillingRequest{
        billingCode: string;
        totalAmount: string;
        paymentStatus: string;
        paymentMethod: string;
        biilingDate: Date;
        paymentDate: Date;
        remarks: string;
        patients: string[];
        treatments: string[];   
}