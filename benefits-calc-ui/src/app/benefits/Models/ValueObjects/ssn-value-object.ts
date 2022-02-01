export class SsnValueObject {
  public constructor(fullSsn: string) {
    this.fullSsn = fullSsn.replace(/\D/g, '');
  }
  public readonly fullSsn: string;
  public getLast4(): string {
    return this.toString(Last4SsnFormatter);
  }
  public get isValid(): boolean {
    return this.fullSsn?.length === 9 && /^\d+$/g.test(this.fullSsn);
  }
  public toString(formatter: SsnFormatter = null): string {
    return formatter ? formatter(this) : this.fullSsn;
  }
  public static parse(fullSsn: string) {
    return new SsnValueObject(fullSsn);
  }
}
export type SsnFormatter = ((ssnVo: SsnValueObject) => string) | null;
export const DefaultSsnFormatter: SsnFormatter = (ssnVo: SsnValueObject) => {
  return ssnVo.fullSsn.split('').reduce((prev, curr, i) => {
    let outStr = prev;
    switch (i) {
      case 4:
      case 2:
        outStr += '-'
        break;
      default: outStr + curr;
    }
    return outStr;
  }, '');
};
export const Last4SsnFormatter: SsnFormatter = (ssnVo: SsnValueObject) => {
  return ssnVo.fullSsn.split('').reduce((prev, curr, i) => {
    let outStr = prev;
    switch (i) {
      case 4:
      case 2:
        outStr += '*-'
        break;
      case 5:
      case 6:
      case 7:
      case 8:
        outStr += curr;
        break;
      default: outStr += '*';
    }
    return outStr;
  }, '');
};
