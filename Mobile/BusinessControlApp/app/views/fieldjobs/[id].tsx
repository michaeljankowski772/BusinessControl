import { useLocalSearchParams, useRouter } from "expo-router";
import { useEffect, useState } from "react";
import {
  ActivityIndicator,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";
import { getFieldJobById, updateFieldJob } from "../../api/fieldjob";
import { COLUMN_LABEL_MAP, FieldJob } from "../../helpers/translations";

export default function FieldJobDetails() {
  const { id } = useLocalSearchParams();
  const router = useRouter();

  const [job, setJob] = useState<FieldJob | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadJob();
  }, []);

  const loadJob = async () => {
    try {
      const data = await getFieldJobById(Number(id));
      setJob(data);
    } catch (err) {
      console.log("LOAD ERROR", err);
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (key: keyof FieldJob, value: any) => {
    if (!job) return;
    setJob({ ...job, [key]: value });
  };

  const handleSave = async () => {
    try {
      if (!job) return;
      await updateFieldJob(job);
      router.back();
    } catch (err) {
      console.log("UPDATE ERROR", err);
    }
  };

  if (loading || !job) {
    return (
      <View style={styles.center}>
        <ActivityIndicator size="large" />
        <Text>Ładowanie...</Text>
      </View>
    );
  }
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Edycja zadania</Text>

      {/* ID (readonly) */}
      <View style={styles.field}>
        <Text style={styles.label}>ID</Text>
        <Text style={styles.readonly}>{job.id}</Text>
      </View>

      {/* Nazwa */}
      <View style={styles.field}>
        <Text style={styles.label}>{COLUMN_LABEL_MAP?.["customerFirstName"] ?? ""}</Text>
        <TextInput
          style={styles.input}
          value={job.customerFirstName}
          onChangeText={(text) => handleChange("customerFirstName", text)}
        />
      </View>

      {/* Status */}
      <View style={styles.field}>
        <Text style={styles.label}>{COLUMN_LABEL_MAP?.["workerFirstName"] ?? ""}</Text>
        <TextInput
          style={styles.input}
          value={job.workerFirstName}
          onChangeText={(text) => handleChange("workerFirstName", text)}
        />
      </View>

      {/* Status */}
      <View style={styles.field}>
        <Text style={styles.label}>{COLUMN_LABEL_MAP?.["workerLastName"] ?? ""}</Text>
        <TextInput
          style={styles.input}
          value={job.workerLastName}
          onChangeText={(text) => handleChange("workerLastName", text)}
        />
      </View>

      {/* Status */}
      <View style={styles.field}>
        <Text style={styles.label}>{COLUMN_LABEL_MAP?.["fieldArea"] ?? ""}</Text>
        <TextInput
          style={styles.input}
          value={job.fieldArea?.toString() ?? ""}
          keyboardType="numeric"
          onChangeText={(text) => {
            let numeric = text.replace(/[^0-9.]/g, "");

            // jeśli jest więcej niż jedna kropka, zostaw tylko pierwszą
            const firstDotIndex = numeric.indexOf(".");
            if (firstDotIndex !== -1) {
              numeric =
                numeric.slice(0, firstDotIndex + 1) +
                numeric.slice(firstDotIndex + 1).replace(/\./g, "");
            }

            setJob((prev) => prev ? { ...prev, fieldArea: Number(numeric) } : prev);
          }}
        />
      </View>

      {/* Zapisz */}
      <TouchableOpacity style={styles.button} onPress={handleSave}>
        <Text style={styles.buttonText}>Zapisz</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  center: { flex: 1, justifyContent: "center", alignItems: "center" },
  container: { flex: 1, padding: 20 },
  title: { fontSize: 22, fontWeight: "bold", marginBottom: 20 },

  field: { marginBottom: 15 },
  label: { marginBottom: 5, fontWeight: "600" },

  input: {
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 8,
    padding: 10,
    backgroundColor: "#fff",
  },

  readonly: {
    padding: 10,
    backgroundColor: "#eee",
    borderRadius: 8,
  },

  button: {
    marginTop: 20,
    backgroundColor: "#007AFF",
    padding: 15,
    borderRadius: 10,
    alignItems: "center",
  },

  buttonText: {
    color: "white",
    fontWeight: "bold",
  },
});