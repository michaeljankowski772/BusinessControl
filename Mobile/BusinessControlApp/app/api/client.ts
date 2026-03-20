import { getAccessToken, getRefreshToken, setTokens } from "../services/tokenService";

const API_URL = process.env.EXPO_PUBLIC_API_URL as string;

export const apiFetch = async (url: string, options: RequestInit = {}): Promise<Response> => {
    let token = await getAccessToken();

    let response = await fetch(`${API_URL}${url}`, {
        ...options,
        headers: {
            ...(options.headers || {}),
            Authorization: token ? `Bearer ${token}` : "",
            "Content-Type": "application/json",
        },
    });

    if (response.status === 401) {
        const refreshToken = await getRefreshToken();

        if (!refreshToken) {
            throw new Error("Brak refresh tokena");
        }

        const refreshResponse = await fetch(`${API_URL}/api/auth/refresh`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ refreshToken }),
        });

        if (!refreshResponse.ok) {
            throw new Error("Sesja wygasła");
        }

        const data = await refreshResponse.json();

        await setTokens(data.token, data.refreshToken);

        response = await fetch(`${API_URL}${url}`, {
            ...options,
            headers: {
                ...(options.headers || {}),
                Authorization: `Bearer ${data.token}`,
                "Content-Type": "application/json",
            },
        });
    }

    return response;
};