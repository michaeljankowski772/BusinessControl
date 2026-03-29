import { useRouter } from "expo-router";
import { useEffect, useState } from "react";
import { ActivityIndicator, Button, TextInput, View } from "react-native";
import { createEmptyFieldJob, getFieldJobById, updateFieldJob } from "../../api/fieldjob";
import { FieldJob } from "../../helpers/translations";

export default function CreateJob() {
  const router = useRouter();

  const [jobId, setJobId] = useState<number | null>(null);
  const [name, setArea] = useState("");
  const [loading, setLoading] = useState(true);
  const [job, setJob] = useState<FieldJob | null>(null);

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

  const handleSave = async () => {
    try {
      if (!job) return;
      await updateFieldJob(job);
      router.back();
    } catch (err) {
      console.log("UPDATE ERROR", err);
    }
  };

  const handleChange = (key: keyof FieldJob, value: any) => {
    if (!job) return;
    setJob({ ...job, [key]: value });
  };

  if (loading) {
    return <ActivityIndicator />;
  }

  //todo add proper form fields, add column headers mapping as itis in [id].tsx
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