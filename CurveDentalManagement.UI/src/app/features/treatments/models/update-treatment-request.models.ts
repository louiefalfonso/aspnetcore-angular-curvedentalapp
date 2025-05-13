export interface UpdateTreatmentRequest{
    treatmentName: string;
    treatmentCode: string;
    description: string;
    duration: string;
    cost: string;
    insuranceCoverage: string;
    insuranceCoverageAmount: string;
    followUpCare: string;
    riskBenefits: string;
    indications: string;
    doctors: string[];
}