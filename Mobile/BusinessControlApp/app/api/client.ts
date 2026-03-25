import { getAccessToken, getRefreshToken, setTokens } from "../services/tokenService";

const API_URL = process.env.EXPO_PUBLIC_API_URL as string;

export const apiFetch = async (url: string, options: RequestInit = {}): Promise<Response> => {
    let token = await getAccessToken();

    console.log("API Fetch - Access Token:", token);

    let response = await fetch(`${API_URL}${url}`, {
        ...options,
        headers: {
            ...(options.headers || {}),
            Authorization: token ? `Bearer ${token}` : "",
            "Content-Type": "application/json",
        },
    });

    console.log("t1", response.status);
    if (response.status === 401) {
        const refreshToken = await getRefreshToken();
        console.log("API Fetch - refresh Token:", refreshToken);

        if (!refreshToken) {
            throw new Error("Brak refresh tokena");
        }

        const refreshResponse = await fetch(`${API_URL}/api/auth/refresh`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ refreshToken }),
        });
        console.log("t2", refreshResponse.status);

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
    else if (!response.ok && response.status !== 401) {
        throw new Error(`HTTP error: ${response.status}`);
    }

    return response;
};