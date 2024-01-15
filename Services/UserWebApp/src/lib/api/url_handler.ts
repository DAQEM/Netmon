import { get } from "svelte/store";

export default class UrlHandler {
    static getUrl(url: string): string {
        return (process.env.BASE_URL || "http://localhost:5000/api") + url;
    }

    static getAccountServiceUrl(url: string): string {
        return this.getUrl(`/account${url}`);
    }
}