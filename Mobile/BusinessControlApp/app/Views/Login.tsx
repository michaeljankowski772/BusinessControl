import { Link, useLocalSearchParams, useRouter } from 'expo-router';
import React, { useState } from 'react';
import { StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';

export default function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const { created } = useLocalSearchParams();
    const router = useRouter();

    const handleLogin = () => {
        if (!username || !password) {
            setError('Proszę wypełnić wszystkie pola');
            return;
        }

        async function parseLogin() {

            try {

                const response = await fetch("https://192.168.0.171:7242/api/auth/login", {
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

                    throw new Error("HTTP error: " + response.status);
                }


                const data = await response.json();

                console.log(data.token);

                //await AsyncStorage.setItem("accessToken", data.accessToken);
                //await AsyncStorage.setItem("refreshToken", data.refreshToken);


                const testResponse = await fetch("https://192.168.0.171:7242/api/auth/test", {
                    headers: {
                        Authorization: "Bearer " + data.token
                    }
                });
                const testData = await testResponse.json();
                console.log(testData.isAuthenticated);
                //router.push("/views/testpanel1");

            } catch (err) {

                console.log("API ERROR:", err);
                setError(err.message);

            }

        }

        parseLogin();


        console.log('Login:', { username, password });
    };

    return (
        <View style={styles.container}>
            {created && (
                <Text style={{ color: "green" }}>
                    Konto zostało utworzone.
                </Text>
            )}
            <Text style={styles.title}>Login</Text>

            {error ? <Text style={styles.error}>{error}</Text> : null}

            <TextInput
                style={styles.input}
                placeholder="Username"
                value={username}
                onChangeText={setUsername}
                keyboardType="default"
            />

            <TextInput
                style={styles.input}
                placeholder="Password"
                value={password}
                onChangeText={setPassword}
                secureTextEntry
            />

            <TouchableOpacity style={styles.button} onPress={handleLogin}>
                <Text style={styles.buttonText}>Sign In</Text>
            </TouchableOpacity>

            <View style={styles.footer}>
                <Text style={styles.footerText}>
                    Don't have an account?{' '}
                    <Link href="/views/register" style={styles.linkText}>
                        Register
                    </Link>
                </Text>
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: { flex: 1, padding: 20, justifyContent: 'center' },
    title: { fontSize: 24, fontWeight: 'bold', marginBottom: 20, textAlign: 'center' },
    input: { borderWidth: 1, borderColor: '#ccc', padding: 10, marginBottom: 15, borderRadius: 5 },
    error: { color: 'red', marginBottom: 15, textAlign: 'center' },
    button: { backgroundColor: '#007AFF', padding: 12, borderRadius: 5, alignItems: 'center' },
    buttonText: { color: 'white', fontSize: 16, fontWeight: 'bold' },
    footer: { marginTop: 16, alignItems: 'center' },
    footerText: { fontSize: 14, color: '#555' },
    linkText: { color: '#007AFF', fontWeight: 'bold' },
});