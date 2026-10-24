import { useLocation, useNavigate } from "react-router-dom"
import { useEffect } from "react";

import { getUrl, checkForRedirection, getProperComponentToRender } from "./appLogic" 

const App = () => {
  const navigate = useNavigate()
  const fullUrl = useLocation().pathname
  const url = getUrl(fullUrl)

  useEffect(() => {
    checkForRedirection(url, navigate);
  }, [url])

  return (
    getProperComponentToRender(url)
  );
}

export default App;
