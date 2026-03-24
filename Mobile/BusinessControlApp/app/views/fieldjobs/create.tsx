import { useRouter } from "expo-router";
import { useState } from "react";
import { Button, TextInput, View } from "react-native";
import { createFieldJob } from "../../api/fieldjob";

export default function CreateJob() {

  const [name, setName] = useState("");
  const router = useRouter();

  const handleCreate = async () => {
    await createFieldJob({ name });
    router.back();
  };

  return (
    <View style={{ padding: 20 }}>
      <TextInput
        placeholder="Job name"
        value={name}
        onChangeText={setName}
        style={{ borderWidth: 1, marginBottom: 10 }}
      />

      <Button title="Create" onPress={handleCreate} />
    </View>
  );
}