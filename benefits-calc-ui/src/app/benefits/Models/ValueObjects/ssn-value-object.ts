export class SsnValueObject {
  public constructor(
    public fullSsn: string
  ){

  }

  public getLast4(): string {
    return this.fullSsn.split('').reduce((prev, curr, i) => {
      let outStr = prev;
      switch(i) {
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
    },'');
  }
  public get isValid(): boolean {
    return this.fullSsn?.length === 9 && /^\d+$/g.test(this.fullSsn);
  }
  public toString(): string {
    return this.fullSsn;
  }
  public static parse(fullSsn: string) {
    return new SsnValueObject(fullSsn);
  }
}
