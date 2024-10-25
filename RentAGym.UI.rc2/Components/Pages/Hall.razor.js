export function onLoad() {
    console.log('Loaded');
}
export function onUpdate() {
    console.log('Updated');
}
export function onDispose() {
    console.log('Disposed');
}
export function changeSlide(slider, slideId) {
    const slide = slider.querySelector(`#${slideId}`);
    if (slide) {
        const containerRect = slider.getBoundingClientRect();
        const slideRect = slide.getBoundingClientRect();
        const scrollOffset = slideRect.left - containerRect.left;
        slider.scrollBy({ left: scrollOffset, top: 0, behavior: 'smooth' });
    }
};
