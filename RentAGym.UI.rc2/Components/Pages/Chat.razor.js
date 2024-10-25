export function scrollToBottomAnimated() {
    const element = document.getElementById("chat-body");
    setTimeout(() => {
        element.scrollTo(0, +(element.scrollHeight + 1000));
    }, 100);
}
