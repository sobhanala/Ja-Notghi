import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom"

import ForgetPasswordPage from "../pages/forget-password-page/forgetPasswordPage"
import LoginPage from "pages/login-page/LoginPage"
import SignUpPage from 'pages/signup-page/SignUpPage'
import ResetPasswordPage from "pages/reset-password-page/ResetPasswordPage"
import PageNotFound from "pages/page-not-found/PageNotFound"
import App from "app/App"
import { MainUrl } from "routes"

const Main = () => {
  return (
    <Router>
      <Routes>
        <Route path={MainUrl.default} element={<Navigate to={MainUrl.loginPage} />} />
        <Route path={MainUrl.loginPage} element={<LoginPage />} />
        <Route path={MainUrl.signUpPage} element={<SignUpPage />} />
        <Route path={MainUrl.forgetPasswordPage} element={<ForgetPasswordPage />} />
        <Route path={MainUrl.resetPasswordPage} element={<ResetPasswordPage />} />
        <Route path={MainUrl.app} element={<App />} />
        <Route path="*" element={<PageNotFound />} />
      </Routes>
    </Router>
  )
}

export default Main
