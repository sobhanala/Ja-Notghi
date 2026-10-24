import { AppUrl } from "routes";

import DefaultAppLayout from "./layouts/default-app-layout/DefaultAppLayout";
import ContentNotFoundLayout from "./layouts/content-not-found-layout/ContentNotFoundLayout";

export const getUrl = (fullPath) => {
	const appRemovedPath = fullPath.substring(4);
	return appRemovedPath;
} 

export const checkForRedirection = (url, navigate) => {
	if (url === "" || url === AppUrl.default) {
		navigate(AppUrl.prefixUrl + AppUrl.timeline);
	}
}

export const getProperComponentToRender = (url) => {
	if (AppUrl.isNotDefined(url)) {
		return <ContentNotFoundLayout />
	}
	else if (AppUrl.isValidUrl(url)) {
		return <DefaultAppLayout />;
	}
} 
