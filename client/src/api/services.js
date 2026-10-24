export const setToken = (token) => {
    localStorage.setItem("token", token);
}

export const getDefaultHeader = () => {
    const token = localStorage.getItem("token");
    const header = {
        'Content-Type': 'application/json',
    ...(token && { Authorization: `Bearer ${token}` })
    };
    return header;
}
