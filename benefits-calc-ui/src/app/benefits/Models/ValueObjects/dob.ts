// import { NumberUtils } from "@app/utilities/number-utils";

// export class Dob {
//   public static readonly maxAge = 100;
//   public static readonly minAge = 18;
//   private static readonly EpochYear = 1970;
//   public constructor(
//     public year: number,
//     public month: Months,
//     public day: number
//   ) {
//     if(!this.isValidDate()) {
//        throw new Error("Invalid Date of Birth.");
//     }
//   }
//   public static fromDate(date: Date): Dob {
//     return new Dob(date.getFullYear(), date.getMonth(), date.getDate());
//   }

//   private get today(): Date {
//     const today = new Date();
//     today.setHours(0,0,0,0);
//     return today;
//   }

//   private get minDate(): Date {
//     const today = this.today;
//     return new Date(today.getFullYear() - Dob.maxAge, today.getMonth(), today.getDate());
//   }

//   private get maxDate(): Date {
//     const today = this.today;
//     return new Date(today.getFullYear() - Dob.minAge, today.getMonth(), today.getDate());
//   }

//   private isValidDate(): boolean {

//     const dobDate: Date = this.toDate();
//     const parsedDateMatches = dobDate.getMonth() === this.month &&
//       dobDate.getDate() === this.day &&
//       dobDate.getFullYear() === this.year;

//     if (!parsedDateMatches) {
//       return false;
//     }

//     return NumberUtils.isInRange(dobDate.getTime(),
//       this.minDate.getTime(),
//       this.maxDate.getTime());
//   }
//   public toDate(): Date {
//     return new Date(this.year, this.month, this.day);
//   }
//   public toString(): string {
//     return this.toDate().toDateString();
//   }
// }
// export enum Months {
//   January = 0,
//   February,
//   March,
//   April,
//   May,
//   June,
//   July,
//   August,
//   September,
//   October,
//   November,
//   December,
// }
