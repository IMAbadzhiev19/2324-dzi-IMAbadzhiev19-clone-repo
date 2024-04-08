import styles from "@/styles/components/Agenda.module.css";

interface Color {
  color: string;
  meaning: string;
}

const Agenda = () => {
  const colors: Color[] = [
    {
      color: "linear-gradient(45deg, #E5A4B4, #FFF689)",
      meaning: "Изтекъл срок на годност и не е в наличност",
    },
    { color: "#FFF689", meaning: "Изтекъл срок на годност" },
    { color: "#E5A4B4", meaning: "Няма в наличност" },
    { color: "#7AE7C7", meaning: "В срок на годност и е в наличност" },
  ];

  return (
    <div className={styles["agenda-wrapper"]}>
      {colors.map((color) => (
        <div key={color.color} className={styles["agenda-container"]}>
          <div
            style={{
              background: color.color,
              width: "25px",
              height: "25px",
              borderRadius: "3em"
            }}
          />
          <p>{color.meaning}</p>
        </div>
      ))}
    </div>
  );
};

export default Agenda;
