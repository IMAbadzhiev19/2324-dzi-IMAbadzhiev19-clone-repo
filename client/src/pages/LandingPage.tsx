import LandingPageHeading from "@/components/LandingPageHeading";
import LandingPageNavbar from "@/components/LandingPageNavbar";
import useAuth from "@/hooks/useAuth";

import styles from "@/styles/pages/LandingPage.module.css";

const LandingPage = () => {
  const { isLoggedIn } = useAuth();

  return (
    <div className={styles["wrapper"]}>
      <main>
        <LandingPageNavbar isSignedIn={isLoggedIn} />
        <LandingPageHeading />
      </main>
      <footer>
        <p>
          &copy; 2023-2024 <span>PMS</span>
        </p>
      </footer>
    </div>
  );
};

export default LandingPage;
