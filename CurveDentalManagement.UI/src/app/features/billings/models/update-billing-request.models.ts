export interface UpdateBillingRequest{
        billingCode: string;
        totalAmount: string;
        paymentStatus: string;
        paymentMethod: string;
        billingStatus: string;
        billingDate: Date;
        paymentDate: Date;
        remarks: string;
        patients: string[];
        treatments: string[];  
}