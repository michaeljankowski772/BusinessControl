import { useRouter } from "expo-router";
import { useEffect, useState } from "react";
import { ActivityIndicator, Button, TextInput, View } from "react-native";
import { createEmptyFieldJob, getFieldJobById, updateFieldJob } from "../../api/fieldjob";

export default function CreateJob() {
  const router = useRouter();

  const [jobId, setJobId] = useState<number | null>(null);
  const [name, setArea] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const init = async () => {
      try {
        const created = await createEmptyFieldJob(); 
        setJobId(created.id);

        const full = await getFieldJobById(created.id);
        setArea(full.fieldArea ?? "");
      } finally {
        setLoading(false);
      }
    };

    init();
  }, []);

  // 🔹 zapis
  const handleSave = async () => {
    if (!jobId) return;

    await updateFieldJob(jobId, { name });
    router.back();
  };

  if (loading) {
    return <ActivityIndicator />;
  }

  return (
    <View style={{ padding: 20 }}>
      <TextInput
        placeholder="Job name"
        value={name}
        onChangeText={setName}
        style={{ borderWidth: 1, marginBottom: 10 }}
      />

      <Button title="Save" onPress={handleSave} />
    </View>
  );
}