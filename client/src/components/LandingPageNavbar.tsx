import styles from "@/styles/components/LandingPageNavbar.module.css";
import { Button } from "./styled/Button";
import { Link } from "react-router-dom";

const LandingPageNavbar = ({ isSignedIn }: { isSignedIn: boolean }) => {
    return (
        <nav className={styles["nav-container"]}>
            <Link to="/" className={styles["link"]}>
                <figure className={styles["logo-container"]}>
                    <img src="/PMS.png" alt="" />
                </figure>
            </Link>
            <div>
                <Link to={isSignedIn ? "home" : "sign-in"}>
                    <Button $primary $animation $line>Започни сега</Button>
                </Link>
            </div>
        </nav>
    );
}

export default LandingPageNavbar;