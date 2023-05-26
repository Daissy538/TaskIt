export class Task {
  public title: string | undefined;
  public description: string | undefined;
  public endDate: Date | undefined;

  constructor(title: string, description: string | undefined, endDate: Date | undefined) {
    this.title = title;
    this.description = description;
    this.endDate = endDate;
  }
}
