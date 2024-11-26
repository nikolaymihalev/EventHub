import { Event } from "./event";

export interface EventPageModel{
    currentPage: number,
    events: Event[],
    pagesCount: number
}