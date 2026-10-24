const ValidUrl = Object.freeze({
    timeline: "/timeline",
    profile: "/profile",
    search: "/search",
    setting: "/setting",
    bookmark: "/bookmark",
    inbox: "/inbox",
    info: "/info",
});

const getValidUrls = () => Object.values(ValidUrl);

const isNotDefined = (url) => {
    return !getValidUrls().includes(url);
}

const isValidUrl = (url) => {
    return getValidUrls().includes(url);
}


const AppUrl = Object.freeze({
    prefixUrl: "/app",
    default: "/",
    timeline: ValidUrl.timeline,
    profile: ValidUrl.profile,
    search: ValidUrl.search,
    setting: ValidUrl.setting,
    bookmark: ValidUrl.bookmark,
    inbox: ValidUrl.inbox,
    info: ValidUrl.info,
    getValidUrls: getValidUrls,
    isNotDefined: isNotDefined,
    isValidUrl: isValidUrl
});

export default AppUrl
