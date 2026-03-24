import { setTokens } from "../services/tokenService";
const API_URL = process.env.EXPO_PUBLIC_API_URL as string;

type IdentityError = {
    code: string;
    description: string;
};

type AuthResponse = {
    token: string;
    refreshToken: string;
};

export const login = async (username: string, password: string): Promise<AuthResponse> => {
    const response = await fetch(`${API_URL}/api/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password })
    });

    if (!response.ok) {
        if (response.status === 401) {
            throw new Error("Nieprawidłowy login lub hasło");
        }
        throw new Error(`HTTP error: ${response.status}`);
    }

    const data: AuthResponse = await response.json();

    await setTokens(data.token, data.refreshToken);
    return data;
};

export const register = async (username: string, password: string): Promise<void> => {
    const response = await fetch(`${API_URL}/api/auth/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ username, password })
    });

    if (!response.ok) {
        let message = "";

        try {
            const errors: IdentityError[] = await response.json();
            message = errors.map(e => e.description).join("\n");
        } catch {
            message = "Unknown error";
        }

        throw new Error(`HTTP error: ${response.status}. ${message}`);
    }
};