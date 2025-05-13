import { Doctor } from "../../doctor/models/doctor.models";

export interface Treatment{
    id: string;
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
    doctors: Doctor[];
}