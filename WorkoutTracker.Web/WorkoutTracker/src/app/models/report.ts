import { User } from './index';

export class Report {
    constructor(
        public IncrId: number,
        public Prev: number,
        public Next: number,
        public IsPrev: boolean,
        public FromDate: string,
        public ToDate: string,
        public Data: Array<number>,
        public User: User
    ) { }
}