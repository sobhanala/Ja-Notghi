export const HardData = {
    profile: "میم",
    username: "ممد ممدنژاد",
    content: " لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است.",
    numOfLikes: 10,
    numOfDislikes: 15,
    numOfComments: 20,
}

export const fetchPosts = (numOfPosts) => {
    const newPosts = []
    for (let i = 0; i < numOfPosts; i++) {
        newPosts.push({
            ...HardData,
            content: `${HardData.content} - پست شماره ${i + 1}`
        })
    }
    return newPosts

};
