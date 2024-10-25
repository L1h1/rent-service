export function changeSlide(slider, slideId) {
    console.log(slideId)
    const slide = document.getElementById(`${slideId}`);
    if (slide) {
        const containerRect = slider.getBoundingClientRect();
        const slideRect = slide.getBoundingClientRect();
        const scrollOffset = slideRect.left - containerRect.left;
        slider.scrollBy({ left: scrollOffset, top: 0, behavior: 'smooth' });
    }
};
