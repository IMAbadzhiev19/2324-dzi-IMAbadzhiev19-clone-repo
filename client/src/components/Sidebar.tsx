import { Link, useLocation, useNavigate, useParams } from "react-router-dom";

import { toast } from "sonner";

import { ListIcon, PenSquare, Settings } from "lucide-react";

import styles from "@/styles/components/Sidebar.module.css";
import { Button } from "./styled/Button";

const routes = [
  {
    label: "Главна стран...",
    icon: ListIcon,
    href: "/pharmacies/:id/dashboard",
    color: "text-yellow-500",
  },
  {
    label: "Настройки",
    icon: Settings,
    href: "/pharmacies/:id/settings",
    color: "text-rose-500",
  },
];

const Sidebar = () => {
  const { id } = useParams();
  const pathname = useLocation().pathname;
  const navigate = useNavigate();

  return (
    <div className={styles["wrapper"]}>
      <header>
        <Link
          to={`/pharmacies/${id}/dashboard`}
          className={styles["header-link"]}
        >
          <div className={styles["media"]}>
            <img src="/PMS.png" alt="Logo" />
          </div>
        </Link>
      </header>
      <div className={styles["routes-container"]}>
        {routes.map((route) => (
          <Link
            to={route.href.replace(":id", id!)}
            key={route.href}
            className={`${styles["route"]} ${
              pathname === route.href.replace(":id", id!)
                ? styles["active-route"]
                : styles["inactive-route"]
            }`}
          >
            <div className={styles["route-info"]}>
              <route.icon
                className={`${styles["route-icon"]} ${styles[route.color]}`}
              />
              <p>{route.label}</p>
            </div>
          </Link>
        ))}
      </div>
      <footer>
        <Button
          $primary
          $animation
          className={styles["exit-btn"]}
          onClick={() => {
            navigate("/pharmacies");
            toast.success("Exit successful");
          }}
        >
          <PenSquare className={styles["change-pharmacy-icon"]} />
          <span>Смени аптеката</span>
        </Button>
      </footer>
    </div>
  );
};

export default Sidebar;
