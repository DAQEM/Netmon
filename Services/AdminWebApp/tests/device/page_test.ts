import test, { expect } from "@playwright/test";

test.describe.parallel("Device page", () => {
    test("Has all devices header", async ({ page }) => {
        await page.goto("/device");

        const header = await page.innerText("h1");
        expect(header).toBe("All Devices");
    });
});