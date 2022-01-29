// import { Dob, Months } from "./dob";

// fdescribe('Dob Value Object Tests', () => {
//   let today: Date;
//   beforeAll(() => {
//     today = new Date();
//     today.setHours(0, 0, 0, 0);
//   });
//   it('should throw error for Feb 29 on non-leap year', () => {
//     expect(() => new Dob(1990, Months.February, 29)).toThrowError("Invalid Date of Birth.");
//   });
//   it("should throw error if 1 day older than 100", () => {
//     const centenarian = new Date(today.getFullYear() - 100, today.getMonth(), today.getDate());
//     centenarian.setDate(centenarian.getDate() - 1);
//     expect(() => Dob.fromDate(centenarian)).toThrowError("Invalid Date of Birth.");
//     expect(() => new Dob(centenarian.getFullYear(), centenarian.getMonth(), centenarian.getDate())).toThrowError("Invalid Date of Birth.");
//   });
//   it("should not throw error if centenarian", () => {
//     const centenarian = new Date(today.getFullYear() - 100, today.getMonth(), today.getDate());
//     expect(() => Dob.fromDate(centenarian)).not.toThrowError("Invalid Date of Birth.");
//     expect(() => new Dob(centenarian.getFullYear(), centenarian.getMonth(), centenarian.getDate())).not.toThrowError("Invalid Date of Birth.");
//   });
//   it('should throw error if younger than 18', () => {
//     const ageOfMajority = new Date(today.getFullYear()-18, today.getMonth(), today.getDate());
//     expect(() => Dob.fromDate(ageOfMajority)).not.toThrowError("Invalid Date of Birth.");
//     ageOfMajority.setDate(ageOfMajority.getDate() - 1);
//     expect(() => Dob.fromDate(ageOfMajority)).toThrowError("Invalid Date of Birth.");
//   });

//   it('should show correct age', () => {
//     const dobDate = new Date(today.getFullYear()-50, today.getMonth(), today.getDate());
//     expect(Dob.fromDate(dobDate).age).toBe(50);
//     dobDate.setDate(dobDate.getDate() - 1);
//     expect(Dob.fromDate(dobDate).age).toBe(49);
//   });

// });
