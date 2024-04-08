import TypewriterComponent from "typewriter-effect";

import styles from "@/styles/components/LandingPageHeading.module.css";

const LandingPageHeading = () => {
  return (
    <div className={styles["container"]}>
      <div className={styles["wrapper"]}>
        <h1>Иновативна система за управление</h1>
        <div className={styles["typewriter"]}>
          <TypewriterComponent
            options={{
              strings: [
                "Умна система.",
                "Управление на аптеки.",
                "Управление на складове.",
                "Леснота на използване.",
                "Интуитивен интерфейс.",
                "Административен панел.",
              ],
              autoStart: true,
              loop: true,
            }}
          />
        </div>
        <div className={styles["img"]}>
          <img
            src="https://img.freepik.com/free-vector/pharmacist_23-2148174862.jpg?size=338&ext=jpg&ga=GA1.1.1546980028.1711152000&semt=ais"
            alt=""
          />
        </div>
      </div>
    </div>
  );
};

export default LandingPageHeading;
