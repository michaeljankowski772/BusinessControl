import { apiFetch } from "./client";

export type FieldJob = {
    id: number;
    name: string;
};

export const getFieldJobs = async (): Promise<FieldJob[]> => {
    const response = await apiFetch("/fieldjobs/getfieldjobs");

    if (!response.ok) {
        throw new Error(`HTTP error: ${response.status}`);
    }

    return response.json();
};