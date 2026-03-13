import { useRouter } from "expo-router";
import React, { useState } from 'react';
import { StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';


export default function Register() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const router = useRouter();

    type IdentityError = {
        code: string
        description: string
    }

    const handleRegister = () => {
        if (!username || !password) {
            setError('Please fill in all fields');
            return;
        }

        async function parseRegister() {

            try {

                const response = await fetch("https://192.168.0.171:7242/api/auth/register", {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({ username, password })
                });


                if (!response.ok) {
                    const errors: IdentityError[] = await response.json();

                    const message = errors.map(e => e.description).join("\n");

                    if (response.status === 401) {
                        throw new Error("Nieprawidłowy login lub hasło. " + message);
                    }

                    throw new Error("HTTP error: " + response.status + ". " + message);
                }

                router.push("/views/login?created=1");

            } catch (err) {

                console.log("API ERROR:", err);
                setError(err.message);

            }

        }

        parseRegister();


        console.log('Login:', { username, password });
    };

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Register</Text>

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

            <TouchableOpacity style={styles.button} onPress={handleRegister}>
                <Text style={styles.buttonText}>Create account</Text>
            </TouchableOpacity>
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
});