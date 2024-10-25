export function onLoad() {
    console.log('Loaded');
}
export function onUpdate() {
    console.log('Updated');
}

export function onDispose() {
    console.log('Disposed');
}

document.addEventListener('mousemove', (e) => {
    try {
        const cat = document.getElementById('cat');
        const rekt = cat.getBoundingClientRect();
        const catX = rekt.left + rekt.width / 2;
        const catY = rekt.top + rekt.height / 2;

        const mouseX = e.clientX;
        const mouseY = e.clientY;
        const angleDeg = angle(mouseX, mouseY, catX, catY);

        const eyes = document.querySelectorAll('.eye');
        eyes.forEach(eye => {
            eye.style.transform = `rotate(${90 + angleDeg}deg)`;
        });
    } catch (error) {
    }
});

function angle(cx, cy, ex, ey) {
    try {
        const dy = ey - cy;
        const dx = ex - cx;
        const rad = Math.atan2(dy, dx);
        const deg = rad * 180 / Math.PI;

        return deg;
    } catch (error) {
        return 0;
    }
}
