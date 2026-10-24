import Post from "app/components/Post/Post";
import { fetchPosts } from "./TimelineMainContentLogic";

import { useEffect, useRef, useState } from "react";

import styles from "./TimelineMainContent.module.css";

const TimelineMaincontent = () => {
    const [posts, setPosts] = useState([])
    const [hasMore, setHasMore] = useState(true)

    const elementRef = useRef(null)

    const onIntersection = (entries) => {
        const firstEntry = entries[0]
        if (firstEntry.isIntersecting && hasMore) {
            setTimeout(() => {
                loadMorePosts();
            }, 2000)
        }
    };

    useEffect(() => {
        const observer = new IntersectionObserver(onIntersection);

        if (elementRef.current) {
            observer.observe(elementRef.current)
        }

        return () => {
            if (observer) {
                observer.disconnect()
            }
        };
    }, [hasMore])

    const loadMorePosts = () => {
        const newPosts = fetchPosts(15)
        if (newPosts.length > 0) {
            setPosts((prevPosts) => [...prevPosts, ...newPosts])
        } else {
            setHasMore(false)
        }
    }

    useEffect(() => {
        loadMorePosts()
    }, [])

    return (
        <div className={styles.postsContainer}>
            {posts.map((post, index) => (
                <Post className={styles.postSize} data={post} key={index} />
            ))}
            <div ref={elementRef} />
            {hasMore &&
                <div className={styles.spinnerContainer}>
                    <div className={styles.spinner}></div>
                    <p className={styles.spinnerText}>در حال بارگذاری...</p>
                </div>
            }
        </div>
    );
};

export default TimelineMaincontent;
